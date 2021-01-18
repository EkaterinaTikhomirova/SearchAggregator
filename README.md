# SearchAggregator

SearchAggregator is a .NET project that wrote with C#. This application search words that were requested by user in three search services (Yandex, Google, Bing) and put it into a database. If user tries to find something that application found before application returns resources from the database. 

## Installation
Use the [link](https://github.com/EkaterinaTikhomirova/SearchAggregator) to install ImageBase. Write code to create repository clone


```bash
gh repo clone EkaterinaTikhomirova/SearchAggregator
```
You need to restore the databases with the file SearchAggregator.bak. You can use [step by step explanation](https://docs.microsoft.com/en-us/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ff660051(v=ws.10)?redirectedfrom=MSDN).

You should change subscription keys for yours in the file appsettings.json.  
For example:
```JSON
"GoogleSearch": {
    "SubscriptionKey": "YOUR-SUBSCRIPTION-KEY",
    "ConfigID": "YOUR-CONFIGURATION-ID"
  }
```

You can get keys via these links [Azure](https://portal.azure.com/), [Yandex](https://xml.yandex.ru/), [Google](https://cse.google.com/cse/all). Please pay attention to requests limits.

## Structure project

_SearchAggregator_ - this project contains rest API request to get resources from search services or from data base;  
_SearchAggregator.Tests_ - this project contains unit tests.

## Usage

Open SearchAggregator.sln and Run _SearchAggregator_ project. Write your request into the input and click on "Search" button. Afterwords you will see ten url-links with short descriptions below.

If all seacher services are unavailable (e.g. all keys are incorrect) you will be notified.  
If nothing were found by each seacher you will be notified as well. 


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
