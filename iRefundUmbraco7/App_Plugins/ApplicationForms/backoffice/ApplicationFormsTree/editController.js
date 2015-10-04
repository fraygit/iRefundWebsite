'use strict';
(function () {
    //create the controller
    function CustomSectionEditController($scope, $routeParams, $http) {
        //set a property on the scope equal to the current route id
        $scope.id = $routeParams.id;
        $scope.test = "mystring";
        $scope.importOutput = "";


        //$scope.runImport = function (importID) {
            console.log("I have called our REST API");

            // here is where we call our Import REST API
            // $scope.importOutput = "Run Successfully!"

            $http.get('surface/ApplicationForm/List').
            success(function (data) {
                console.log(data);
                //$scope.importOutput = data.content;
            });
        //}

    };
    //register the controller
    angular.module("umbraco").controller('CustomSectionEditController', CustomSectionEditController);

})();