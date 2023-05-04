# SessionShare
Share session between two application 

Steps to replicate the issue 
1) Run the both the project. (configure it from project solution to execute multiple project at once)
2) Visit web form application by using https://localhost:44386/About. In page_load event session values is available
3) Visit web api by using https://localhost:7069/WeatherForecast/GetWeatherForecast. in API method, it is trying to get session which is available on web form project
