# Lab 2 - Container Apps
In this lab you set up a microservice architecture to plan Microsoft Exams.

## 1. Register docker containers
### 1. Setup Container Registry
1. Navigate to `Create resource`
2. Search for `Container Registry`
3. Choose `Create`
4. Enter the following values:

| Field          | Value               |
|----------------|---------------------|
| Subscription   | MTech               |
| Resource group | YOUR_RESOURCE_GROUP |
| Registry name  | YOUR_REGISTRY_NAME  |
| Location       | West Europe         |
| SKU            | Basic               |

### 2. Configure Registry permissions
1. Open the Container Registry that was just created
2. Open `Access Keys` section
3. Check `Admin user` checkbox


### 3. Build docker images
1. Clone [exam-microservices project](https://github.com/MaartenGDev/exam-microservices)
2. Open the project in a terminal
3. Build the images using:
4. `az acr build --registry YOUR_REGISTRY_NAME --image exam-catalog:v1 ./exam-catalog`
5. `az acr build --registry YOUR_REGISTRY_NAME --image exam-shop:v1 ./exam-shop`
6. `az acr build --registry YOUR_REGISTRY_NAME --image exam-planner:v1 ./exam-planner`


## 2. Setup first Container App
1. Navigate to `Create resource`
2. Search for `Container App`
3. Choose `Create`
4. Enter the following values in `Basics`:

| Field                      | Value               |
|----------------------------|---------------------|
| Subscription               | MTech               |
| Resource group             | YOUR_RESOURCE_GROUP |
| Container App name         | exam-catalog        |
| Region                     | West Europe         |
| Container Apps Environment | *Use default value* |

5. Click on `Next: App Settings`
6. Enter the following values:

| Field                | Value                                 |
|----------------------|---------------------------------------|
| Use quickstart image | False                                 |
| Image source         | Azure Container Registry              |
| Registry             | YOUR_REGISTRY_NAME.azurecr.io         |
| Image                | exam-catalog                          |
| Image tag            | v1                                    |
| CPU and Memory       | 0.25 CPU cores, 0.5 Gi Memory         |
| Ingress              | **Enabled**                           |
| Ingress Traffic      | Limited to Container Apps Environment |
| Ingress type         | HTTP                                  |
| Transport            | Auto                                  |
| Target port          | 3000                                  |

7. Click `Create`

## 3. Setup `exam-shop` container
1. Open searchbar in the portal
2. Search for `Container Apps Environments`
3. Choose the environment that was just created
4. Navigate to `Apps` section
5. Choose `Create`
6. Enter the following values in `Basics`:

| Field                      | Value               |
|----------------------------|---------------------|
| Subscription               | MTech               |
| Resource group             | YOUR_RESOURCE_GROUP |
| Container App name         | exam-shop           |
| Region                     | West Europe         |
| Container Apps Environment | *Use default value* |

7. Click on `Next: App Settings`
8. Enter the following values:

| Field                | Value                                 |
|----------------------|---------------------------------------|
| Use quickstart image | False                                 |
| Image source         | Azure Container Registry              |
| Registry             | YOUR_REGISTRY_NAME.azurecr.io         |
| Image                | exam-shop                             |
| Image tag            | v1                                    |
| CPU and Memory       | 0.25 CPU cores, 0.5 Gi Memory         |
| Ingress              | **Enabled**                           |
| Ingress Traffic      | Limited to Container Apps Environment |
| Ingress type         | HTTP                                  |
| Transport            | Auto                                  |
| Target port          | 3001                                  |

9. Choose `Create`

## 3. Setup `exam-planner` container
1. Open searchbar in the portal
2. Search for `Container Apps Environments`
3. Choose the environment that was just created
4. Navigate to `Apps` section
5. Choose `Create`
6. Enter the following values in `Basics`:

| Field                      | Value               |
|----------------------------|---------------------|
| Subscription               | MTech               |
| Resource group             | YOUR_RESOURCE_GROUP |
| Container App name         | exam-planner        |
| Region                     | West Europe         |
| Container Apps Environment | *Use default value* |

7. Click on `Next: App Settings`
8. Enter the following values:

| Field                | Value                           |
|----------------------|---------------------------------|
| Use quickstart image | False                           |
| Image source         | Azure Container Registry        |
| Registry             | YOUR_REGISTRY_NAME.azurecr.io   |
| Image                | exam-planner                    |
| Image tag            | v1                              |
| CPU and Memory       | 0.25 CPU cores, 0.5 Gi Memory   |
| Ingress              | **Enabled**                     |
| Ingress Traffic      | Accepting traffic from anywhere |
| Ingress type         | HTTP                            |
| Transport            | Auto                            |
| Target port          | 3002                            |

8. Add the following environment variables using `Environment Variables > + Add`
- `CATALOG_SERVICE_URL` with the Ingress http url of the `exam-catalog` Container App
- `SHOP_SERVICE_URL` with the Ingress http url of the `exam-shop` Container App

9. Choose `Create`

## 4. Visit the `exam-planner` web app
1. Open in link showed after `Application Url` label


## 5. (Extra) Test multiple revisions
### Configure revision management
1. Navigate to `exam-planner` Container App in the Azure Portal
2. Open `Revision Management`
3. Click `Choose revision mode` option in menu
4. Choose `Multiple: Several revisions active simultaneously`

### Create new `exam-planner` version
1. Open local terminal in the `exam-microservices` project
2. Open `exam-planner/index.js`
3. Change `<h1>Available exams</h1>` to `<h1>Workshop exams</h1>`
4. Build v2 of the exam-planner: `az acr build --registry YOUR_REGISTRY_NAME --image exam-planner:v2 ./exam-planner`

### Enable v2 in `exam-planner` Container App
1. Navigate to `exam-planner` Container App in the Azure Portal
2. Open `Containers`
3. Choose `Edit and deploy`
4. Select `exam-planner` and choose `Edit`
5. Change `Image tag` from `v1` to `v2` in side-panel
6. Click `Save`

### Split traffic 50/50 between revisions
1. Open `Revision Management`
2. Enter `50` as value for `Traffic` for both revisions
3. Choose `Save`
4. Open Container App url displayed in `Overview`
5. Refresh multiple times to see the different revisions being served
