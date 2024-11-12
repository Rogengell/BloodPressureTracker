pipeline{
    agent any
    triggers{
        pollSCM('* * * * *')
    }
    stages{
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