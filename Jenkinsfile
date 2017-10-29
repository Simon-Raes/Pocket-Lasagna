#!/usr/bin/env groovy

def workspace

node('(unity&&gui)') {
  if (env.BRANCH_NAME.startsWith('feature/') || env.BRANCH_NAME.startsWith('bugfix/')) {
    print "Feature and Bugfix branches should not be built automatically; cancelling."
    return
  }

  workspace = pwd()

  stage('Checkout') {
    deleteDir()
    checkout scm
  }

  stage('Build') {
    sh '/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -batchmode -logFile -projectPath ' + workspace + ' -buildWindows64Player ./build/windows/ITPOffice.exe'
  }

  stage('Deploy') {
    sh 'echo Deploying...'
    //archiveArtifacts artifacts: 'output/*.ipa,output/*.dSYM.zip', allowEmptyArchive: true
  }

}
