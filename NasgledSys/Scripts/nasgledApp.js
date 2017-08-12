(function () {

    var app = angular.module('nasgledApp', ['ngRoute']);

    app.controller('HomeController', function ($scope) {
        $scope.messege = "Angular has beeen included";
    }
        );
})();