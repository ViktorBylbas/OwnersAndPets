OaPApp.controller('PetsController', function PetsController($scope, $http, $routeParams) {
    var ownerId = $routeParams["id"];

    $scope.currentPage = 0;
    $scope.pageSize = 3;

    outputTotalCount(ownerId);
    outputPets(ownerId);

    $http.post('/Pets/GetOwner', { Id: ownerId }).then(function (data) {
        $scope.owner = data.data;
    });

    $scope.addPet = function (petName, formPets) {
        if (formPets.$valid) {
            addPet(petName, ownerId);
            $scope.petName = null;
        }
        else
            alert("Enter pet`s name");
    }

    function addPet(petName, ownerId) {
        $http.post('/Pets/AddPet', { Name: petName, OwnerId: ownerId }).then(function (data) {
            console.log(data.data);
            outputTotalCount(ownerId);
            outputPets(ownerId);
        });
    }

    function outputTotalCount(ownerId) {
        $http.post('/Pets/GetTotalCount', { OwnerId: ownerId }).then(function (data) {
            $scope.totalCount = data.data;
        });
    }

    function outputPets(ownerId) {
        $http.post('/Pets/GetPets', { Id: ownerId }).then(function (data) {
            $scope.pets = data.data;

            $scope.numberOfPages = function () {
                var num = Math.ceil($scope.pets.length / $scope.pageSize);
                num = (num == 0) ? 1 : num;

                return num;
            }
        });
    }

    $scope.delPet = function (petId, ownerId) {
        delPet(petId, ownerId);
    }

    function delPet(petId, ownerId) {
        $http.post('/Pets/DeletePet', { Id: petId }).then(function (data) {
            console.log(data.data);
            outputTotalCount(ownerId);
            outputPets(ownerId);
        });
    }
});