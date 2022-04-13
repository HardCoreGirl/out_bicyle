FROM nginx

# root 에 app 폴더를 생성
RUN mkdir /app

# work dir 고정
WORKDIR /app

# work dir 에 build 폴더 생성 /app/build
RUN mkdir ./build

# host pc의 현재 경로의 build 폴더를 workdir의 build 폴더로 복사
ADD ./WebGL ./build

# host pc의 nginx.conf를 아래 경로에 복사
COPY ./nginx.conf /etc/nginx/conf.d 

# 80번 포트 오픔
EXPOSE 80
 
# container 실행시 자동으로 실행할 command, ngnix 시작함
CMD ["nginx", "-g", "daemon off;"]