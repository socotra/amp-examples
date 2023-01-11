ARG arch=amd64
FROM --platform=linux/${arch} node:12.18-alpine AS base
WORKDIR /app

FROM node:12.18-alpine AS build
WORKDIR /src
COPY package*.json ./
COPY . .
RUN npm install
RUN npm run build

FROM base AS final
ENV NODE_ENV=production
COPY --from=build /src/dist/ /app/dist/
COPY ./*.json /app/
RUN npm install --production=false --silent && mv node_modules ../
EXPOSE 3000
CMD [ "node", "dist/main.js" ]
