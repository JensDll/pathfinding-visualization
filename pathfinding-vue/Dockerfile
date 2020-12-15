FROM alpine:3

LABEL maintainer="Jens Doellmann"

RUN apk add --update nodejs npm

COPY . /app

WORKDIR /app

RUN npm install

EXPOSE 3000

CMD ["npm", "run", "dev"]