FROM mcr.microsoft.com/mssql/server:2022-latest

# Use root to install packages
USER root

# Clean existing repository configurations to avoid duplicates
RUN rm -f /etc/apt/sources.list.d/microsoft-prod.list /etc/apt/sources.list.d/mssql-release.list

# Install required tools
RUN apt-get update && apt-get install -y curl gnupg2

# Add Microsoft repository key
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg

# Add Microsoft SQL Server repository
RUN echo "deb [arch=amd64,arm64,armhf signed-by=/usr/share/keyrings/microsoft-prod.gpg] https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022 jammy main" > /etc/apt/sources.list.d/mssql-server.list

# Try to install Full-Text Search (if available)
RUN apt-get update && \
    (ACCEPT_EULA=Y apt-get install -y mssql-server-fts || echo "FTS package not available, continuing without it")

# Revert back to default mssql user
USER mssql

EXPOSE 1433

CMD ["/opt/mssql/bin/sqlservr"]
