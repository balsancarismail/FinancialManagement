pipeline {
    agent any

    environment {
        DOCKER_HUB_CREDENTIALS_ID = 'dockerhub'
        IMAGE_NAME = 'snc22bx7/financial-management'
        DISABLE_AUTH = 'true'
        DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Build .NET Core Project') {
            steps {
                script {
                    docker.image('mcr.microsoft.com/dotnet/nightly/sdk:8.0').inside {
                        echo "DISABLE_AUTH is ${DISABLE_AUTH}"
                        sh 'dotnet build --configuration Release'
                    }
                }
            }
        }
        // Unit Test Stage
        stage('Run Unit Tests') {
            steps {
                script {
                    docker.image('mcr.microsoft.com/dotnet/nightly/sdk:8.0').inside {
                        // Varsayılan olarak, test projesinin dizinine gitmeniz gerekebilir
                        // sh 'cd YourTestProjectDirectory'
                        sh 'dotnet test --logger "trx;LogFileName=unit_tests_results.xml"'
                    }
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

        stage('Deploy') {
            steps {
                script {
                    sh "docker-compose -f ${WORKSPACE}/docker-compose.yml up -d"
                }
            }
        }
    }
}
