FROM azuresdk/azure-cli-python:latest
WORKDIR /root/.azure

ADD https://github.com/openfaas/faas/releases/download/0.7.0/fwatchdog /usr/bin
RUN chmod +x /usr/bin/fwatchdog

ENV fprocess="xargs az"

HEALTHCHECK --interval=5s CMD [ -e /tmp/.lock ] || exit 1

CMD ["fwatchdog"]