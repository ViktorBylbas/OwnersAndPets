OaPApp.controller('OwnerController', function OwnerController($scope, $http) {
    $scope.currentPage = 0;
    $scope.pageSize = 3;

    outputTotalCount();
    outputOwners();

    $scope.addOwner = function (ownerName, formOwners) {
        if (formOwners.$valid) {
            addOwner(ownerName);
            $scope.ownerName = null;
        }
        else
            alert("Enter owner`s name");
    }

    function addOwner(ownerName) {
        $http.post('/Home/AddOwner', { Name: ownerName }).then(function (data) {
            console.log(data.data);
            outputTotalCount();
            outputOwners();
        });
    }

    function outputTotalCount() {
        $http.post('/Home/GetTotalCount').then(function (data) {
            $scope.totalCount = data.data;
        });
    }

    function outputOwners() {
        $http.post('/Home/GetOwners').then(function (data) {
            $scope.owners = data.data;

            $scope.numberOfPages = function () {
                var num = Math.ceil($scope.owners.length / $scope.pageSize);
                num = (num == 0) ? 1 : num;

                return num;
            }
        });
    }

    $scope.delOwner = function (ownerId) {
        delOwner(ownerId);
    }

    function delOwner(ownerId) {
        $http.post('/Home/DeleteOwner', { Id: ownerId }).then(function (data) {
            console.log(data.data);
            outputTotalCount();
            outputOwners();
        });
    }
});