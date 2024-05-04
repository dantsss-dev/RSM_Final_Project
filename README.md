# RSM_Final_Project

## GETTING STARTED WITH THE PROJECT

First we are going to verify if we have installed docker, because is one of the key parts to make the project works correctly,
we can check if we have docker installed runing the command
```bash
docker --version
```
if you dont have docker installed, i'm going to leave you the link to the main website
https://www.docker.com/get-started/
There you can find the right installer for you OS.

After you get docker installed in your machine, we are going to create a new Redis Image for us to use it on the project, just follow the next commands
```bash
docker run --name my-redis -p 5002:6379 -d redis
```
This is going to create a knew redis image named my-redis, that it will be listening the port 5002, you can choose another port if you like just make sure
it is pointing on the port 6379 that is the original port of the redis server.

```bash
docker exec -it my-redis sh
```
Now, we are going to interact with our redis container, here we are going to open redis-cli to check if everything is going well with our services
```bash
redis-cli
```
With just typing ping, the redis cli will return a PONG if everything is okey, now we can acces to or cache database based on Key->value
```bash
ping
select 0
dbsize
scan 0
```
The first command select the first database in the redis server, then we could check the size of the database an for last, we could scan whats inside of the database.
That will be all for setting our redis cache server, now we are going to verify if we have installed the next packages in our .net project
```bash
dotnet add package StackExchange.Redis --version 2.7.33
dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis --version 8.0.4
```
Now we are going to install the packages for generate the reports in our application
```bash
dotnet add package QuestPDF
```
That will be all for installing dependencies in the side of the Backend, now we have to configure our appsetting.json to connect with both our database on sqlServer and Redis.

![imagen](https://github.com/dantsss-dev/RSM_Final_Project/assets/135795866/45b28d90-4c2a-4046-a7ef-60035dad25ef)

In the appsetting.json you are going to have a configuration like this, all you have to do is change the DefaultConnection string to point to your database and on the Redis connection,
you just have to change, the port of the localhost for the one you set in the previous step.

And... We are set to running or backend services, you just has to build and run the project to test it in postman, i will leave the link to the collection i already build for the test, it is pretty simple,
it just has 4 request methods.
Remembered when you run the next commands that your console is pointing to the FinalProyect carpet
```bash
dotnet build
dotnet run 
```
https://www.postman.com/aerospace-operator-10717020/workspace/rsm-final-proyect/collection/33327958-dfd5e27b-2ba1-45a5-b622-8f4202606968?action=share&creator=33327958

## Frontend

Finally we are going to set up or Frontend to communicate with the services and have the full functionality of the app.

here we just have to make sure that we have installed node.js and it has to be a version superior to the 16.20 because on the contrary the frontend will just not goes,
so you can check your version of node with the nex command
```bash
node -v
```
and also i will leave the link if you don't have it installed yet
https://nodejs.org/en/download

now we are all set up to test our full application, just open another console besides the one that is running the backend services y make sure that it is pointing to the final-project carpet
```bash
npm run dev
```
with this we finished to setting up our project, so test it i leave me your thoughts and feedback, it will be very appreciated.
