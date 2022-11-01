# Lab 3 - Blob Storage Account
In this lab you will respond to changes in a storage account

## 1. Create storage account
### 1. Setup account
1. Navigate to `Create resource`
2. Search for `Storage Account`
3. Choose `Create`
4. Enter the following values:

| Field                | Value                            |
|----------------------|----------------------------------|
| Subscription         | MTech                            |
| Resource group       | YOUR_RESOURCE_GROUP              |
| Storage account name | YOUR_UNIQUE_STORAGE_ACCOUNT_NAME |
| Region               | West Europe                      |
| Performance          | Standard                         |
| Redundancy           | LRS                              |

5. Choose `Review`
6. Choose `Create`

### 2. Create container
1. Navigate to `Containers` section
2. Click `+ Container`
3. Enter the following values:

| Field               | Value                         |
|---------------------|-------------------------------|
| Name                | images                        |
| Public access level | private (no anonymous access) |

### Upload test file
1. Open `images` container
2. Choose `Upload`
3. Upload `test_image.jpg` from `resources` folder

## 2. Create Azure Function
### 1. Setup
1. Navigate to `Create resource`
2. Search for `Function App`
3. Choose `Create`
4. Enter the following values:

| Field             | Value                     |
|-------------------|---------------------------|
| Subscription      | MTech                     |
| Resource group    | YOUR_RESOURCE_GROUP       |
| Function App Name | YOUR_UNIQUE_FUNCTION_NAME |
| Region            | West Europe               |
| Publish           | Code                      |
| Runtime stack     | .NET                      |
| Version           | 6                         |
| Region            | West Europe               |
| Operating System  | Windows                   |
| Plan Type         | Consumption               |

5. Choose `Review + create`
6. Choose `Create` 

### 2. Create Function
1. Open `Functions` section
2. Choose `Create`
3. Select `Azure Blob Storage Trigger`
4. Enter the following values:

| Field                      | Value                                       |
|----------------------------|---------------------------------------------|
| New Function               | ImageProcessingTrigger                      |
| Path                       | images/{name}                               |
| Storage account connection | *New*                                       |
| Storage account connection | Select storage account created in Chapter 1 |

5. Click `Create` and wait for the resource to be created
6. Open `Integration` section
6. Click `Outputs` > `Add Output`
7. Enter the following values:

| Field                      | Value                                       |
|----------------------------|---------------------------------------------|
| Binding Type               | Azure Blob Storage                          |
| Storage account connection | Select storage account created in Chapter 1 |
| Blob parameter name        | outputBlob                                  |
| Path                       | generated-images/{rand-guid}                |


### 3. Upload function code
1. Open `Code+Test` section
2. Click `Upload` and select `function.proj` from `resources` folder
3. Open the dropdown and select `function.proj`
4. Paste the content of `resources/function.proj` in the visible editor
5. Click `Save`
6. Click `Upload` and select `azure_logo.jpg` from `resources` folder
7. Paste the content of `run.csx` from `resources` folder in the visible text editor
8. Click on `Save`

### 4. Test function
1. Click on `Test/Run`
2. Enter `images/test_image.jpg` in the `body` field
3. Click `Run`
4. Check console output. It should show `Executed 'Functions.ImageProcessingTrigger' (Succeeded)`
5. Open a new browser tab
6. Navigate to the storage account you created in Chapter 1
7. Open `Containers` section
8. Open `generated-images` container
9. Click on the first record
10. Choose `Download` and view the file locally

## 3. Use Azure function app
1. Navigate to the storage account you created in Chapter 1
2. Open `Containers` section
3. Open `images` container
4. Choose `Upload`
5. Upload `test_image2.jpg` from `resources` folder
6. Navigate back to the `Containers` section by clicking on `... | Containers` link in the breadcrumb
7. Wait a minute, a new image should appear in the `generated-images` container of the storage account
8. Choose `Download` and view the file locally
