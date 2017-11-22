angular.module('nasgledApp')

.controller('jsInstituteController', function ($scope, InstituteInfo) {
    $scope.CompanyList = null;
    InstituteInfo.GetAllInstitute().then(function (d) {
        $scope.CompanyList = d.data;
    },
    function (error) {
        alert('Sorry, no records can be loaded at this moment.');
    });
})
.factory('InstituteInfo', function ($http) {
    var fac = {};
    fac.GetAllInstitute = function () {
        return $http.get('/DL_Institute/GetAllInstitute')
    }
    return fac;
});