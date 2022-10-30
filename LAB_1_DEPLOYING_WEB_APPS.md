# Lab 1 - Deploying Web Apps
In this lab you set up a WebApp with monitoring and Blob Storage integration

## 1. Fork the sample app
1. Open [https://github.com/MaartenGDev/web-app-monitoring](https://github.com/MaartenGDev/web-app-monitoring)
2. Fork repository to your own account

## 2. Create WebApp
1. Navigate to `Create resource`
2. Search for `WebApp`
3. Choose `Create`
4. Enter the following values:

| Field            | Value               |
|------------------|---------------------|
| Subscription     | MTech               |
| Resource group   | YOUR_RESOURCE_GROUP |
| Name             | YOUR_APP_NAME       |
| Publish          | Code                |
| Runtime stack    | .NET 6 (LTS)        |
| Operating System | Windows             |
| Region           | West Europe         |
| Windows Plan     | **Default value**   |

5. For `Sku and size` click `Change size`
6. Choose `Dev / Test`
7. Choose `D1`
8. Click `Next: Deployment`
9. Choose `Enable` for `Continuous deployment`
10. Login using your GitHub account
11. Enter the following values:

| Field        | Value               |
|--------------|---------------------|
| Organisation | YOUR_GITHUB_ACCOUNT |
| Repository   | web-app-monitoring  |
| Branch       | main                |

12. Click `Preview file` to view generated YAML pipeline
13. Choose `Close`
14. Click `Next: Networking`
15. Click `Next: Monitoring`

6Enter the following values:

| Field                       | Value             |
|-----------------------------|-------------------|
| Enable Application Insights | Yes               |
| Application Insights        | **Default value** |
| Region                      | **Default value** |

16. Choose `Review + Create`
17. Choose `Create`

## 2. View deployment status in GitHub
1. Open your fork of the `web-app-monitoring` on GitHub
2. Navigate to `Actions` tab
3. Open the `Build and deploy ASP.Net Core app to Azure Web App` workflow
4. Wait for the workflow to complete

## 3. Open deployed site
1. Open the just created Web App in Azure
2. Navigate to `Overview` section
3. Click on `URL` to open site
4. You should get a json response
5. Navigate to `/failing`
6. You should get a server error

## 4. Debug production issues
1. Open a new tab with the azure portal (`portal.azure.com`)
2. Open your resource group
3. Open the WebApp that was just created
4. Open `Application Insights` section
5. Click `View Application Insight data`
6. Checkout the graphs that shown recent errors
7. Navigate to `Failures`
8. Open the row containing `GET WeatherForecast/GetFailingWeatherForecast`
9. Choose `500` in `Top 3 response codes`
10. Choose the first sample operation
11. Click on the first `EXCEPTION` record
12. View the `Exception Properties` section to view the exception message

## 5. Deploy fix
1. Open your forked project on GitHub
2. Navigate to `WebAppMonitoring.Api/Controllers/WeatherForecastController.cs`
3. Choose the `Edit` action by clicking on `Pen Icon`
4. Remove the `Throw new InvalidOperationException` line at line 38.
5. Choose `Commit Changes`
6. Navigate to `Actions` tab
7. Wait for workflow to finish

## 6. Test fix
1. Open your Azure Portal tab
2. Open your resource group
3. Open the created WebApp
4. Open the URL
5. Navigate to `/failing`
6. You should get a json response

## 7. Setup Blob Container for news
1. Open your Azure Portal tab
2. Open your resource group
3. Choose `Create`
4. Search for `Storage Account`
5. Choose `Create`
6. Enter the following values:

| Field                | Value                            |
|----------------------|----------------------------------|
| Subscription         | MTech                            |
| Resource group       | YOUR_RESOURCE_GROUP              |
| Storage account name | YOUR_UNIQUE_STORAGE_ACCOUNT_NAME |
| Region               | West Europe                      |
| Performance          | Standard                         |
| Redundancy           | LRS                              |

7. Choose `Review`
8. Choose `Create`

## 8. Create a news file in Blob Storage
1. Navigate to `Containers`
2. Click `+ Container`
3. Enter the following values:

| Field               | Value   |
|---------------------|---------|
| Name                | news    |
| Public access level | Private |

4. Choose `Create`
5. Create a file named `news.txt` on your desktop
6. Fill the `news.txt` file with a news message
7. Open Azure Portal tab
8. Click `Upload`
9. Select `news.txt`
10. Choose `Upload`
11. Navigate to `Access Keys` in the sidebar
12. Click `Show` for `Key1 > Connection string`
13. Click copy next to the shown value

## 9. Configure Web App
1. Navigate to your Resource Group
2. Open the Web App
3. Open `Configuration`
4. Click `+ New application setting`
5. Enter the following values

| Field | Value                                           |
|-------|-------------------------------------------------|
| Name  | Storage__ConnectionString                       |
| Value | Paste the value you copied in chapter 8 step 13 |

6. Choose `OK`
7. Choose `Save`

## 10. Test your Web App with Blob Storage connection
1. Navigate to the `Overview` section of your Web App
2. Open the url shown after the `URL` label
3. Navigate to `/news`
4. You should see the message you placed in the `news.txt` file

## 11. Checkout Resource Map
1. Open the Web App detail page in Azure
2. Navigate to `Application Insights`
3. Click on `View Application Insights data`
4. Open `Application Map`
5. See the link between WebApp and Blob Storage