/**
* Created with Systematize.
* User: WilburZ
* Date: 2014-04-08
* Time: 01:37 AM
* To change this template use Tools | Templates.
*/

var systematize = angular.module('systematize', []);
function mainController($scope, $http) {
    
    $scope.formData = {};
    $scope.showTaskAddRow = false;
    $scope.showJournalAdded = false;
    $scope.editTemplateRow = 0;
    $scope.addedJournal = "";
    $scope.selectedJournalId = 0;
    $scope.SelectedJournalName = "";
    
    $http.get('/journals/')
        .success(function(data) {
            $scope.journals = data;
        })
        .error(function(err) {
            console.error(err);
        });
    
    $http.get('/tasks/1')
        .success(function(data) {
            $scope.tasks = data;
        })
        .error(function(err) {
            console.error(err);
        });
    
    $http.get('/sessions/1')
        .success(function(data) {
            $scope.sessions = data;
        })
        .error(function(err) {
            console.error(err);
        });
    
    $scope.createJournal = function() {
        var btn = $('#createJournal');
        btn.button('loading');
        $http.post('/journals/', $scope.journalData)
             .success(function(data) {
                 $scope.addedJournal = $scope.journalData.name;
                 $scope.journalData = {};
                 $scope.journals = data;
                 $scope.showJournalAdded = true;
                 
                 btn.button('reset');
                
                 setTimeout(function() { $('#addJournal').modal('hide'); }, 3000);            
             })
             .err(function(err) {
                 console.error(err);
             });
    };
    
    $scope.selectJournal = function(id) {
        $http.get('/journals/' + id)
            .success(function(data) {
                $scope.selectedJournalId = data._id;
                $scope.selectedJournalName = data.name;
            })
            .err(function(err) {
                console.error(err);
            });
    };
    
    $scope.appendTaskRow = function() {
        if(!$scope.showTaskAddRow)
            $scope.showTaskAddRow = true;
        $scope.$broadcast('showTaskAddFocus');
    };
    
    $scope.cancelTaskAdd = function() {
        if($scope.showTaskAddRow)
            $scope.showTaskAddRow = false;    
    };
    
    $scope.createTask = function() {
        $http.post('/tasks/1', $scope.formData)
            .success(function(data) {
                $scope.formData = {};
                $scope.tasks = data;
                $scope.showTaskAddRow = false;
            })
            .error(function(err) {
                console.error('Error: ' + err);
            });
    };
    
    $scope.editTask = function(id) {
        //need to finish this up and take the id of the row and show the edit template.
        //Also need to create the edit template.
        //http://jsfiddle.net/siliconball/QwDn9/2/
        var span = $('span#' + id);
        var td = span.parent();
        var name = td.siblings('#task-value-'+id);
        name.append('<div><input type="text" id="' + id + '" />');
    };
    
    $scope.deleteTask = function(id) {
        $http.delete('/tasks/1/' + id)
            .success(function(data) {
                $scope.tasks = data;
                console.log(data);
            })
            .error(function(err) {
                console.log('Error: ' + err); 
            });
    };
}