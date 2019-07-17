; (function () {
    "use strict";

    var dbDiagnosticsModule = angular.module('DbDiagnosticsModule', ["kendo.directives"]);

    dbDiagnosticsModule.config(['$locationProvider', function ($locationProvider) {
        // In order to get the query string from the
        // $location object, it must be in HTML5 mode.
        $locationProvider.html5Mode(true);
    }]);

    dbDiagnosticsModule.controller("DbDiagnosticsController", ['$scope', '$http', '$sce', '$location', '$window', "$timeout",
        function ($scope, $http, $sce, $location, $window, $timeout) {
            var sf_appPath = $window.sf_appPath || "/";

            $scope.databaseTables = [];
            $scope.indexFragmentation = [];
            $scope.databaseSize = null;

            $scope.dbTablesGridOptions = {
                dataSource: {
                    pageSize: 50
                },
                scrollable: false,
                sortable: true,
                pageable: true,
                columns: [{
                    field: "Name",
                    title: "Name",
                    width: "120px"
                }, {
                    field: "Size",
                    title: "Size",
                    width: "120px"
                }, {
                    field: "Rows",
                    title: "Rows",
                    width: "120px"
                }]
            };

            $scope.indexFragmentationGridOptions = {
                dataSource: {
                    pageSize: 50
                },
                scrollable: false,
                sortable: true,
                pageable: true,
                columns: [{
                    field: "Name",
                    title: "Name",
                    width: "120px"
                }, {
                    field: "Database",
                    title: "Database",
                    width: "120px"
                }, {
                    field: "Table",
                    title: "Table",
                    width: "120px"
                }, {
                    field: "Fragmentation",
                    title: "Fragmentation",
                    width: "120px"
                }, {
                    field: "FillFactor",
                    title: "FillFactor",
                    width: "120px"
                }]
            };

            $scope.getDatabaseTables = function () {
                $http.get(sf_appPath + 'db-diagnostics/database-tables')
                    .then(function successCallback(response) {
                        if (response.data && response.data.result) {
                            $scope.databaseTables = response.data.result;
                            $scope.dbTablesGrid.dataSource.data($scope.databaseTables);
                        }
                    });
            };

            $scope.getIndexFragmentation = function () {
                $http.get(sf_appPath + 'db-diagnostics/index-fragmentation')
                    .then(function successCallback(response) {
                        if (response.data && response.data.result) {
                            $scope.indexFragmentation = response.data.result;
                            $scope.indexFragmentationGrid.dataSource.data($scope.indexFragmentation);
                        }
                    });
            };

            $scope.getDatabaseSize = function () {
                $http.get(sf_appPath + 'db-diagnostics/database-size')
                    .then(function successCallback(response) {
                        if (response.data && response.data.result) {
                            $scope.databaseSize = response.data.result;
                        }
                    });
            };

            $scope.rebuildIndexes = function () {
                $scope.showLoading = true;
                $http.get(sf_appPath + 'db-diagnostics/rebuild-indexes')
                    .then(function successCallback(response) {
                        if (response.data && response.data.result) {
                            $scope.showLoading = false;
                            if (response.data.result.StatusCode === 200) {
                                $scope.rebuildIndexesSuccessMsg = true;
                                $timeout(function () {
                                    $scope.rebuildIndexesSuccessMsg = false;
                                }, 3000);
                            }
                        }
                    });
            };

        }]);
})();