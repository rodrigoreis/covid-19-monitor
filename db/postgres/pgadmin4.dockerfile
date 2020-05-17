FROM dpage/pgadmin4:latest
COPY ./pgadmin4-servers.json /pgadmin4/servers.json
EXPOSE 80 443
ENTRYPOINT ["/entrypoint.sh"]