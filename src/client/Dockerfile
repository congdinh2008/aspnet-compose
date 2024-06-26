# Stage 1: Build the Angular app
FROM node:latest AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build --configuration=production

# Stage 2: Serve the app with nginx
FROM nginx:alpine
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /app/dist/todo-app/browser /usr/share/nginx/html/
COPY /nginx.conf  /etc/nginx/conf.d/default.conf

EXPOSE 4200