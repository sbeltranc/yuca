FROM golang:1.22-alpine AS builder

# set the working directory inside the container
WORKDIR /app

# dependencies management
COPY go.mod go.sum ./
RUN go mod download

# copying source code to build
COPY . .

# arguments passed through for building
ARG TARGETOS=linux
ARG TARGETARCH=amd64
ARG VERSION=dev
ARG COMMIT=none

# building the applications
# some comments for the args to understand by my good friend copilot:
# - CGO_ENABLED=0: Disables Cgo to create a statically linked binary. This is crucial for running in minimal base images like distroless.
# - -ldflags: Injects build-time variables into the binary.
# - -o /app/server: Specifies the output file name.
RUN GOOS=${TARGETOS} GOARCH=${TARGETARCH} CGO_ENABLED=0 \
    go build -ldflags="-X 'main.version=${VERSION}' -X 'main.commit=${COMMIT}'" -o /app/server .

# creating the final image we are gonna use
# for running yuca
FROM gcr.io/distroless/static-debian12
WORKDIR /app

# copying the compiled binary from the builder stage
COPY --from=builder /app/server /app/server

# shouldn't be doing this in the 1st place
# but eh, it works atm
COPY .env .

# only enable his for debugging
# you're supposed to be using tunnels or something idk
# EXPOSE 3000

# runing as nonroot user for security reasons
USER yuca
ENTRYPOINT ["/app/server"]
