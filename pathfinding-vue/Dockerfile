FROM node:15 as build
LABEL maintainer="Jens Doellmann"
WORKDIR /app
COPY package.json package-lock.json docker-entrypoint.sh ./

FROM build as dev
ENTRYPOINT ["sh", "docker-entrypoint.sh"]
CMD ["npm", "run", "dev"]

#TODO: build step production