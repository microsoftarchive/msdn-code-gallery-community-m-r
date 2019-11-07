using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFaceIdentify
{
    class Program
    {

        //FaceService Client
        static FaceServiceClient Clnt = new FaceServiceClient(Properties.Settings.Default.FaceKey);

        static void Main(string[] args)
        {

            while (true)
            {
                doAction();
            }

            Console.ReadLine();
        }

        static async void doAction()
        {
            try
            {
                //Group Information
                string groupId = "c01aad66-94fe-416c-98d2-7d6a7a228508";
                string groupName = "Group01";

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1: Training");
                Console.WriteLine("2: Query");

                var choice = Console.ReadKey();

                if (choice.KeyChar == '1')
                {
                    Console.WriteLine();
                    Console.WriteLine("Starting training");
                    System.IO.DirectoryInfo diRoot = new System.IO.DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images"));

                    //Creating group
                    var group = await createGroup(groupId, groupName);

                    foreach (var folder in diRoot.GetDirectories())
                    {
                        //Getting all training foto
                        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(folder.FullName);
                        List<string> images = new List<string>();
                        di.GetFiles().ToList().ForEach(f => images.Add(f.FullName));

                        //Add person to group and related images
                        addPersonToGroup(groupId, folder.Name, images);

                        Console.WriteLine("Adding " + folder.Name);

                        //Used to prevent demo license threshold
                        System.Threading.Thread.Sleep(30000);
                    }

                    //Train group
                    trainPersonGroup(groupId);
                }
                else
                {
                    //Foto to be identified
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Type image path");
                    string fotoPath = Console.ReadLine();
                    fotoPath = fotoPath.Replace("\"", string.Empty);



                    //Identify person 
                    var res = identifyPersons(groupId, fotoPath);

                    //Get Person information
                    foreach (var p in res.Result)
                    {
                        if (p.Candidates.Length > 0)
                        {
                            var pers = getPersonById(groupId, p.Candidates[0].PersonId);
                            pers.Wait();

                            Console.WriteLine(pers.Result.Name + " - " + pers.Result.PersistedFaceIds.Length + " - " + p.Candidates[0].PersonId.ToString() + " - " + p.Candidates[0].Confidence);

                        }
                        else
                        {
                            Console.WriteLine("Unknow person");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
                else
                    Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        /// <summary>
        /// Return a PersonObject by his ID and groupId
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        private static async Task<Person> getPersonById(string groupId, Guid personId)
        {
            try
            {
                var t = Clnt.GetPersonAsync(groupId, personId);
                t.Wait();

                return t.Result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        /// <summary>
        /// Train the PersonGroup
        /// </summary>
        /// <param name="groupId"></param>
        private static async void trainPersonGroup(string groupId)
        {
            try
            {
                //var tStatus = Clnt.GetPersonGroupTrainingStatusAsync(groupId);
                //tStatus.Wait();

                //var status = tStatus.Result.Status;

                var t = Clnt.TrainPersonGroupAsync(groupId);
                t.Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Identify a person by his image within a specific PersonGroup
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="fotoPath"></param>
        /// <returns></returns>
        private static async Task<IdentifyResult[]> identifyPersons(string groupId, string fotoPath)
        {

            try
            {
                Task<Face[]> tDetect = Clnt.DetectAsync(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(fotoPath)));
                tDetect.Wait();

                List<IdentifyResult> result = new List<IdentifyResult>();

                if (tDetect.Result != null && tDetect.Result.Length > 0)
                {


                    List<Guid> faceIds = new List<Guid>();

                    tDetect.Result.ToList().ForEach(t => faceIds.Add(t.FaceId));

                    Task<IdentifyResult[]> tIdent = Clnt.IdentifyAsync(groupId, faceIds.ToArray());
                    tIdent.Wait();

                    if (tIdent.Result != null && tIdent.Result.Length > 0)
                    {
                        return tIdent.Result;
                    }
                    else
                        throw new ApplicationException("Person not found");

                }
                else
                    throw new ApplicationException("No Faces");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Add a person and his training images to a specific group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="personName"></param>9
        /// <param name="imagesPath"></param>
        private static async void addPersonToGroup(string groupId, string personName, List<string> imagesPath)
        {
            try
            {
                Task<AddPersistedFaceResult>[] tAdds = new Task<AddPersistedFaceResult>[imagesPath.Count];

                //Create a person
                var p = Clnt.CreatePersonAsync(groupId, personName, personName);
                p.Wait();
                var p1 = p.Result;

                //Adding person images
                for (int i = 0; i < imagesPath.Count; i++)
                {
                    System.IO.Stream ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(imagesPath[i]));

                    tAdds[i] = Clnt.AddPersonFaceAsync(groupId, p1.PersonId, ms);
                }

                Task.WaitAll(tAdds);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Get or create a PersonGroup
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private static async Task<PersonGroup> createGroup(string groupId, string groupName)
        {
            Task<PersonGroup> result = null;

            //Trying to get group specified by groupId
            try
            {
                var tGet = Clnt.GetPersonGroupAsync(groupId);
                tGet.Wait();
                result = tGet;
            }
            catch (Exception)
            {
                //If the group does not exist, I create IT

                Task tCreate = Clnt.CreatePersonGroupAsync(groupId, groupName);
                tCreate.Wait();

                var tGet = Clnt.GetPersonGroupAsync(groupId);
                tGet.Wait();
                result = tGet;
            }

            return result.Result;
        }
    }
}
