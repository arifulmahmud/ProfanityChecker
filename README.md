# ProfanityChecker
A project to check profanity words in a uploaded file, ie. Text is the supported format at this moment. This project contains two sub-projects. Each project contains individual README.md file with futher instructions on running the projects.

### Run ProfanityAPI project

ProfanityAPI, is an ASP.NET Web API application for checking profanity in a uploaded file. Exposes [api/profanity] for uploading file and get the reponse of checking. Only supported method is POST, and the file is attached as form-data.

### Run ProfanityClientApp project

ProfanityClientApp is a simple React app for demonstrating the usages of profanity API. It's currently consuming the live API endpoint running at : http://profanityapp.azurewebsites.net/api/profanity in Azure.
