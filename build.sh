export VERSION=$(git describe --tags --always --abbrev=0 --dirty)
export COMMIT=$(git rev-parse --short HEAD)

docker build \
  --build-arg VERSION=${VERSION} \
  --build-arg COMMIT=${COMMIT} \
  -t yuca-server .

docker run -p 3000:3000 yuca-server
