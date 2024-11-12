pipeline{
    agent any
    triggers{
        pollSCM('* * * * *')
    }
    environment {
        DOCKER_COMPOSE = '/usr/local/bin/docker-compose'  // Path to docker-compose binary
    }
    stages{
        stage('Checkout'){
            steps{
                checkout scm
            }
        }
        stage('Build'){
            steps{
                echo 'Building the application'
                sh 'docker --version'
                sh 'docker-compose build'
            }
        }
        stage('prepare test'){
            steps{
                sh 'docker-compose up -d bloodpressure'
                sh 'docker-compose up -d patient'
                sh 'docker-compose up -d mesurment'
                sh 'docker-compose up -d featurehub'
                sh 'docker-compose up -d migration_service'
            }
        }
        stage('Test'){
            steps{
                echo 'Testing the application'
            }
        }
        stage('Deploy'){
            steps{
                echo 'Deploying the application'
            }
        }
    }
}