What is MyCompany?
My Company is a set of sample applications comprised of typical enterprise/business modules: Travel, Staff, Vacation, Visitors and Expenses.

Test Mode

Most of the apps use Windows Azure Active Directory for authentication. The app can avoid this dependency, for example, for demos where there is not Internet connection or not to configure WAAD.
This mechanism is called “test mode” where the apps works without authentication. If the test mode is activated the application will use the user Andrew Davis by default.

By default all apps are in test mode.

	Web Support
	To start (F5) the Web Application from Visual Studio with the test model activated, in the web project set the specific page property to “NoAuth”.
	If the web application is already running it´s possible to use the test mode adding the work “NoAuth” at the end of the URI, for example, http://localhost:port/noauth

	Windows Store Support
	In the setting page of the windows Store Apps (Expenses and Visitors) it´s possible to activate the test mode. The application remembers these settings so the next time the application will start in test mode.
	By default Windows Store apps have the test mode enabled

	Desktop Support
	To activate the mode in the Desktop application (MyCompany.Travel.Client.Desktop) you need to change the test mode application setting inside the configuration file (app.config). Set this property to true to activate the test mode.



