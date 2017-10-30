# Images
Web application that allows a user to upload an image, view/search/rate the images, post comments. Login with facebook.

## Backend
WebApi + Owin. MediatR for messaging bus between the Api and the business layer. File based storage for images and data.

## Frontend
AngularJs, Angular Material, Typescript, SASS and WebPack. 

## Starting the app
Frontend (you need node.js to start in dev mode or build the app):
```
cd frontend
npm install
npm run start
```
At this point it will open a tab that points to http://localhost:6002/.

Backend: open `backend/Backend.sln` and start the `Backend` webapi project. The default storage location is `C:\data` and can be changed in the `backend/Backend/Web.config` file, `fs:storagePath` key.
