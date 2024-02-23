pipeline {
    agent any

    environment {
        DOCKER_HUB_CREDENTIALS_ID = 'dockerhub'
        IMAGE_NAME = 'snc22bx7/financial-management'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build and Test') {
            steps {
                script {
                    // Docker Compose ile uygulamanızı ve bağımlılıklarını başlatın
                    sh 'docker-compose -f docker-compose.yml up -d --build'

                    // Burada test komutlarınızı çalıştırın
                    // Örneğin: sh 'docker-compose exec webapp dotnet test'

                    // Docker Compose ile servisleri durdurun
                    sh 'docker-compose down'
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    def commitId = sh(script: "git rev-parse --short HEAD", returnStdout: true).trim()
                    docker.build("${IMAGE_NAME}:${commitId}")
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                script {
                    docker.withRegistry('https://index.docker.io/v1/', DOCKER_HUB_CREDENTIALS_ID) {
                        def commitId = sh(script: "git rev-parse --short HEAD", returnStdout: true).trim()
                        docker.image("${IMAGE_NAME}:${commitId}").push()
                    }
                }
            }
        }
    }
}
