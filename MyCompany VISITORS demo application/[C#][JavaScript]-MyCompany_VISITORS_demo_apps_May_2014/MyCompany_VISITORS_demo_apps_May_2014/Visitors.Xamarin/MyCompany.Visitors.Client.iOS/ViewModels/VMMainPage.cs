using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using MyCompany.Visitors.Client.DocumentResponse;
using MyCompany.Visitors.Client.Model;
using MyCompany.Visitors.Client.ViewModel.Base;
#if __IOS__
using MyCompany.Visitors.Client.DocumentResponse;
#endif

namespace MyCompany.Visitors.Client.ViewModels
{
	public class VMMainPage : VMBase
	{
		public static VMMainPage MainPage;
		const int ITEMS_TO_RETRIEVE_PAGEZERO = 0;
		const int ITEMS_TO_RETRIEVE_TODAY = 1000;
		const int ITEMS_TO_RETRIEVE_OTHER = 12;
		const int ITEMS_TO_RETRIEVE_LIVETILE = 5;
		const int TODAY_GROUP_ID = 1;
		const int OTHER_GROUP_ID = 2;

        IMyCompanyClient clientService = null;

        IMyCompanyClient ClientService
        {
            get
            {
#if __IOS__
                clientService = MyCompany.Visitors.Client.iOS.AppDelegate.CompanyClient;
#else
				clientService = MyCompany.Visitors.Client.Droid.MainActivity.CompanyClient;

#endif
                return clientService;

            }
            set
            {
                clientService = value;
            }

        }

		HubConnection hubConnection;

		VMVisit nextVisit;
		int numberOfLoads;
		int otherVisitsCount;
		string otherVisitsHeader;
		ObservableCollection<VMVisit> othersVisits;

		bool showOtherVisits = true;
		bool showTodayVisits = true;
		ObservableCollection<VMVisit> todayVisits;
		int todayVisitsCount;
		string todayVisitsHeader;
		ObservableCollection<VisitsGroup> visits;
		ObservableCollection<VMEmployee> employees;
		ObservableCollection<VMVisitor> visitors;
		/// <summary>
		///     Default constructor.
		/// </summary>
		public VMMainPage()
		{
			MainPage = this;

            #if __IOS__
            MyCompany.Visitors.Client.iOS.AppDelegate.VMMainPage = MainPage;
            #endif

			InitializeNotifications();
		}

		/// <summary>
		///     Visits List.
		/// </summary>
		public ObservableCollection<VisitsGroup> VisitsList
		{
			get { return visits; }
			set
			{
				visits = value;
				RaisePropertyChanged(() => VisitsList);
			}
		}
		public ObservableCollection<VMEmployee> Employees
		{
			get { return employees; }
			set
			{
				employees = value;
				RaisePropertyChanged(() => Employees);
			}
		}
		public ObservableCollection<VMVisitor> Visitors
		{
			get { return visitors; }
			set
			{
				visitors = value;
				RaisePropertyChanged(() => Visitors);
			}
		}

		/// <summary>
		///     Today next visit
		/// </summary>
		public VMVisit NextVisit
		{
			get { return nextVisit; }
			set
			{
				if (nextVisit == value)
					return;
				nextVisit = value;
				base.RaisePropertyChanged(() => NextVisit);
			}
		}

		/// <summary>
		///     Today visits
		/// </summary>
		public ObservableCollection<VMVisit> TodayVisits
		{
			get { return todayVisits; }
			set
			{
				if (todayVisits == value)
					return;
				todayVisits = value;
				base.RaisePropertyChanged(() => TodayVisits);
			}
		}

		/// <summary>
		///     Today visits
		/// </summary>
		public ObservableCollection<VMVisit> OtherVisits
		{
			get { return othersVisits; }
			set
			{
				othersVisits = value;
				base.RaisePropertyChanged(() => OtherVisits);
			}
		}

		/// <summary>
		///     This property is true if there are no visits the other days, false otherwise.
		/// </summary>
		public bool ShowOtherVisits
		{
			get { return showOtherVisits; }
			set
			{
				showOtherVisits = value;
				base.RaisePropertyChanged(() => ShowOtherVisits);
			}
		}

		/// <summary>
		///     This property is true if there are no visits today, false otherwise.
		/// </summary>
		public bool ShowTodayVisits
		{
			get { return showTodayVisits; }
			set
			{
				showTodayVisits = value;
				base.RaisePropertyChanged(() => ShowTodayVisits);
			}
		}

		/// <summary>
		///     This property is true if there isn't any visit to show.
		/// </summary>
		public bool ShowNextVisit
		{
			get { return !(!showTodayVisits && !showOtherVisits); }
		}

		/// <summary>
		///     Today visits count.
		/// </summary>
		public int TodayVisitsCount
		{
			get { return todayVisitsCount; }
			set
			{
				todayVisitsCount = value;
				base.RaisePropertyChanged(() => TodayVisitsCount);
				base.RaisePropertyChanged(() => TodayVisitsHeader);
			}
		}

		/// <summary>
		///     Other visits count
		/// </summary>
		public int OtherVisitsCount
		{
			get { return otherVisitsCount; }
			set
			{
				otherVisitsCount = value;
				base.RaisePropertyChanged(() => OtherVisitsCount);
				base.RaisePropertyChanged(() => OtherVisitsHeader);
			}
		}

		/// <summary>
		///     Hub today visits header
		/// </summary>
		public string TodayVisitsHeader
		{
			get
			{
				todayVisitsHeader = string.Format("Today visits ({0})", TodayVisitsCount);
				return todayVisitsHeader;
			}
		}

		/// <summary>
		///     Hub other visits header
		/// </summary>
		public string OtherVisitsHeader
		{
			get
			{
				otherVisitsHeader = "Other Visits";
				return otherVisitsHeader;
			}
		}

		/// <summary>
		///     Hub next visit header
		/// </summary>
		public string NextVisitHeader
		{
			get { return "Next Visit"; }
		}

        public void InitializeData(IMyCompanyClient clientService)
        {
            this.ClientService = clientService;
            InitializeData();
        }


		/// <summary>
		///     Initialize Data method.
		/// </summary>
		/// <returns></returns>
		public async void InitializeData()
		{
			try
			{
				if (numberOfLoads >= 0)
					IsBusy = true;

				VisitsList = new ObservableCollection<VisitsGroup>();
				TodayVisits = new ObservableCollection<VMVisit>();
				OtherVisits = new ObservableCollection<VMVisit>();
				Employees = new ObservableCollection<VMEmployee>();
				Visitors = new ObservableCollection<VMVisitor>();
				// Today visits.
				int todayItems = await GetTodayVisits();
				// Other visits.
				int otherItems = await GetOtherVisits();

				int visitors = await GetVisitors();

				int employeItems = await GetEmployees();
				// Update next visit visibility.
				RaisePropertyChanged(() => ShowNextVisit);

				// Update the Tiles.
				await UpdateLiveTileData();
				CreateVisitsGroup(todayItems, otherItems);
				numberOfLoads++;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		/// <summary>
		///     Dispose method.
		/// </summary>
		/// <param name="dispose"></param>
		protected override void Dispose(bool dispose)
		{
			MessengerInstance.Unregister(this);
		}

		void CreateVisitsGroup(int todayItems, int otherItems)
		{
			if (todayItems > 0)
			{
				VisitsList.Add(new VisitsGroup
				{
					GroupId = TODAY_GROUP_ID,
					GroupName = "Todays Visits",
					Items = new ObservableCollection<VMVisit>(TodayVisits)
				});
			}
			if (otherItems > 0)
			{
				VisitsList.Add(new VisitsGroup
				{
					GroupId = OTHER_GROUP_ID,
					GroupName = "Other Visits",
					Items = new ObservableCollection<VMVisit>(OtherVisits)
				});
			}

			RaisePropertyChanged(() => VisitsList);
		}

		/// <summary>
		///     Get the three next today's visits.
		/// </summary>
		/// <returns></returns>
		async Task<int> GetTodayVisits()
		{
			DateTime fromDate = DateTime.Now.AddHours(-1).ToUniversalTime();
			DateTime toDate = DateTime.Today.AddDays(1).ToUniversalTime();

			IList<Visit> todayResults =
				await
					ClientService.VisitService.GetVisits(string.Empty, PictureType.All, ITEMS_TO_RETRIEVE_TODAY,
						ITEMS_TO_RETRIEVE_PAGEZERO, fromDate, toDate);

			TodayVisits = new ObservableCollection<VMVisit>(todayResults.Select(v => new VMVisit(v, TODAY_GROUP_ID)));
			int todayItems = 3;
			var nextVisit = TodayVisits.FirstOrDefault(v => v.VisitDate >= fromDate);

			NextVisit = nextVisit ?? null;
			ShowTodayVisits = todayResults.Any();
			TodayVisitsCount = todayItems;

			return todayItems;
		}

		/// <summary>
		///     This method get the 9 first visits others than today visits.
		/// </summary>
		/// <returns></returns>
		async Task<int> GetOtherVisits()
		{
			IList<Visit> otherResults =
				await
					ClientService.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, ITEMS_TO_RETRIEVE_OTHER,
						ITEMS_TO_RETRIEVE_PAGEZERO, DateTime.Today.AddDays(1).ToUniversalTime());
			
			int otherItems = 35;

			ShowOtherVisits = otherResults.Any();
			OtherVisits = new ObservableCollection<VMVisit>(otherResults.Select(v => new VMVisit(v, OTHER_GROUP_ID)));

			if (otherResults.Any() && NextVisit == null)
			{
				// Set the next visit of another day.
				int id = otherResults.First().VisitId;
				Visit nextVisit = await ClientService.VisitService.Get(id, PictureType.Big);
				NextVisit = new VMVisit(nextVisit, TODAY_GROUP_ID);
			}
			OtherVisitsCount = otherItems;

			return otherItems;
		}

		async Task<int> GetEmployees()
		{
			try
			{
				IList<Employee> results = await this.ClientService.EmployeeService.GetEmployees("", PictureType.Small, 30, 0);
				if (results != null)
					Employees = new ObservableCollection<VMEmployee>(results.Select(x => new VMEmployee{Employee = x}));
				return employees.Count;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return 0;
			}
		}

		async Task<int> GetVisitors()
		{
			IList<Visitor> results = await this.ClientService.VisitorService.GetVisitors("", PictureType.Small, 100, 0);
			if (results != null)
				Visitors = new ObservableCollection<VMVisitor>(results.Select(x => new VMVisitor {Visitor = x}));
			return Visitors.Count;
		}

		public async Task<bool> Save(VMVisit vmVisit)
		{
			try
			{
				var visit = new Visit
				{
					VisitId = vmVisit.VisitId,
					EmployeeId = vmVisit.Employee.EmployeeId,
					VisitorId = vmVisit.VisitorId,
					VisitDateTime = vmVisit.VisitDate,
					Comments = vmVisit.Comment,
					HasCar = vmVisit.HasCar,
					Plate = vmVisit.Plate,
				};
				if (visit.VisitId > 0)
				{
					await this.ClientService.VisitService.Update(visit);
					return true;
				}

				var result = await this.ClientService.VisitService.Add(visit);
				return true;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				return false;
			}
		}


		void InitializeNotifications()
		{
			try
			{
				//signalr notifications
				hubConnection = new HubConnection(AppSettings.ApiUri.ToString(), new Dictionary<string, string>
				{
					{"isNoAuth", AppSettings.TestMode.ToString()}
				});

				if (!AppSettings.TestMode)
					hubConnection.Headers.Add("Authorization", AppSettings.SecurityToken);

				IHubProxy hubProxy = hubConnection.CreateHubProxy("VisitorsNotificationHub");
				hubProxy.On<Visit>("notifyVisitAdded", OnVisitAdded);
				hubProxy.On<Visit>("notifyVisitArrived", OnVisitArrived);
				hubProxy.On<ICollection<VisitorPicture>>("notifyVisitorPicturesChanged", OnVisitorPicturesChanged);
				hubConnection.Start().ContinueWith(
					task =>
					{
						if (task.IsCompleted)
						{
						}
					});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		async void OnVisitArrived(Visit visit)
		{
			bool hasArrived = visit.Status == VisitStatus.Arrived;
			int visitorId = visit.VisitorId;

			IEnumerable<VMVisit> visitsWithChangedVisitor;
			foreach (VisitsGroup visitGroup in VisitsList)
			{
				visitsWithChangedVisitor = visitGroup.Items.Where(v => v.VisitorId == visitorId);
				foreach (VMVisit v in visitsWithChangedVisitor)
				{
					v.HasArrived = hasArrived;
				}
			}

			visitsWithChangedVisitor = OtherVisits.Where(v => v.VisitorId == visitorId);
			if (visitsWithChangedVisitor.Any())
			{
				foreach (VMVisit v in visitsWithChangedVisitor)
				{
					v.HasArrived = hasArrived;
				}

			}


			visitsWithChangedVisitor = TodayVisits.Where(v => v.VisitorId == visitorId);
			if (visitsWithChangedVisitor.Any())
			{
				foreach (VMVisit v in visitsWithChangedVisitor)
				{
					v.HasArrived = hasArrived;
				}

			}

			if (NextVisit.VisitorId == visitorId)
			{
				NextVisit.HasArrived = hasArrived;
			}
		}

		async void OnVisitAdded(Visit visit)
		{
			{
				bool isNext = NextVisit == null || visit.VisitDateTime < NextVisit.VisitDate;

				bool isToday = visit.VisitDateTime.ToLocalTime().Date == DateTime.Now.Date;
				ObservableCollection<VMVisit> list = isToday ? TodayVisits : OtherVisits;
				int maxNumberOfItems = isToday ? ITEMS_TO_RETRIEVE_TODAY : ITEMS_TO_RETRIEVE_OTHER;
				int visitGroup = isToday ? TODAY_GROUP_ID : OTHER_GROUP_ID;
				var VMVisit = new VMVisit(visit, visitGroup);

				VMVisit previousVisit = list.LastOrDefault(v => v.VisitDate < VMVisit.VisitDate);

				int visitIndex = previousVisit == null ? 0 : list.IndexOf(previousVisit) + 1;

				bool hasToBeAdded = visitIndex <= list.Count;
				bool needToRemoveLast = list.Count() == maxNumberOfItems;

				if (isNext)
					NextVisit = VMVisit;

				if (hasToBeAdded)
				{
					list.Insert(visitIndex, VMVisit);
				}

				if (needToRemoveLast)
				{
					list.Remove(list.Last());
				}

				if (isToday)
				{
					ShowTodayVisits = true;
					TodayVisitsCount++;
				}
				else
				{
					ShowOtherVisits = true;
					OtherVisitsCount++;
				}

				RaisePropertyChanged(() => ShowNextVisit);
			}
		}

		async Task UpdateLiveTileData()
		{
			if (!IsInDesignMode)
			{
				IList<Visit> visitsToShowInTile =
					await
						ClientService.VisitService.GetVisitsFromDate(string.Empty, PictureType.Small, ITEMS_TO_RETRIEVE_LIVETILE,
							ITEMS_TO_RETRIEVE_PAGEZERO, DateTime.Now.ToUniversalTime());
			}
		}


		async void OnVisitorPicturesChanged(ICollection<VisitorPicture> visitorPictures)
		{
			{
				int visitorId = visitorPictures.First().VisitorId;

				IEnumerable<VMVisit> visitsWithChangedVisitor;
				foreach (VisitsGroup visitGroup in VisitsList)
				{
					visitsWithChangedVisitor = visitGroup.Items.Where(v => v.VisitorId == visitorId);
					foreach (VMVisit visit in visitsWithChangedVisitor)
					{
						visit.ChangePhotos(visitorPictures);
					}
				}

				visitsWithChangedVisitor = OtherVisits.Where(v => v.VisitorId == visitorId);
				if (visitsWithChangedVisitor.Any())
				{
					foreach (VMVisit visit in visitsWithChangedVisitor)
					{
						visit.ChangePhotos(visitorPictures);
					}
				}


				visitsWithChangedVisitor = TodayVisits.Where(v => v.VisitorId == visitorId);
				if (visitsWithChangedVisitor.Any())
				{
					foreach (VMVisit visit in visitsWithChangedVisitor)
					{
						visit.ChangePhotos(visitorPictures);
					}
				}

				if (NextVisit.VisitorId == visitorId)
				{
					NextVisit.ChangePhotos(visitorPictures);
				}

				foreach (var vmVisitor in visitors.Where(x=> x.Visitor.VisitorId == visitorId))
				{
					vmVisitor.ChangePhotos(visitorPictures);
				}
			} 
		}
	}
}