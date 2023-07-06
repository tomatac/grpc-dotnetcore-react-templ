echo Creating proto files for javascript application in folder: ../ClientApp/src/protos-built
mkdir -p ../ClientApp/src/protos-built

protoc -I=./ greet.proto \
       --plugin=protoc-gen-grpc-web=/usr/local/bin/protoc-gen-grpc-web \
       --js_out=import_style=commonjs,binary:..//ClientApp/src/protos-built \
       --grpc-web_out=import_style=typescript,mode=grpcweb:..//ClientApp/src/protos-built
