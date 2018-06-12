## Getting started

### Trainer project

The SentimentAnalysis.Trainer project containers a .net Core console app that will allow you to update/train the existing model, if you wish to update/add-to the existing dataset, you can do so by updating the "train.tsv" file, under the /data directory.

### Web project

Add two Environment Variables:

BlobKey = The primary key for Blob Storage account
AppInsightsKey = Application Insights telemetry key

The SentimentAnalysis.Web project is a ASP.NET core webapi project that you can use to expose the trained model from a rest end-point, currently, it retrieves the trained model from Azure Blob Storage, so you can 'hot swap' trained models in Production.

The rest api implements IoC and in-memory caching for serving models in Production.

### Hosting the project

You can use the Dockerfile in the SentimentAnalysis.Web project, to build and deploy the SentimentAnalysis.Web project as a Docker Container, currently tested and advised to host using Azure Container Instances.

#### Contributing

Please submit a PR if you wish to add something to grow this project, also, feel free to submit an issue for any questions/bugs you come across.

### TODOs

. Tests
<br />
. Hosting model and using Cosmos DB to train/update model in real-time