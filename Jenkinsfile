pipeline{
    agent any
    triggers{
        pollSCM('* * * * *')
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
                echo 'prepare test'
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
                sh'dotnet test'
            }
        }
        stage('Deploy'){
            steps{
                echo 'Deploying the application'
            }
        }
        stage('Clean'){
            steps{
                sh 'docker-compose down'
            }
        }
    }
}