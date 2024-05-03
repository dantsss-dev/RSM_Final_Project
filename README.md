# RSM_Final_Project

SETTING REDIS CACHE ENVIROMENT AND DEPENDENCIES
//create a container with redis image
docker run --name my-redis -p 5002:6379 -d redis 

//check containers
docker ps -a

//interact with redis container
docker exec -it my-redis sh
//open redis cli on docker instance
redis-cli

//on the cli of redis
//verify if redis is up
ping
//select the first database on the list
select 0
//check size of the db
dbsize
//check items on the list
scan 0

/*---------------------------------------*/
Install NuGet Packages for redis

dotnet add package StackExchange.Redis --version 2.7.33
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis --version 8.0.4

INSTALLING QUESTPDF DEPENDENCIES

//working with QuestPdf

//package manager
Install-Package QuestPDF

//or net cli
dotnet add package QuestPDF

FRONTEND

//install node latest version through choco package manager or from website
choco install nodejs-lts --version="20.12.2"
https://nodejs.org/en/download

//Configuering tailwindcss
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init -p
npm install @heroicons/react
