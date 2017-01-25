var OaPApp = angular.module("OwnersAndPetsApp", ["ngRoute"]).config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/owners', {
        templateUrl: 'views/owners.html',
        controller: 'OwnerController'
    });

    $routeProvider.when('/pets/:id', {
        templateUrl: 'views/pets.html',
        controller: 'PetsController'
    });

    $routeProvider.otherwise({redirectTo: '/owners'});

    $locationProvider.html5Mode(true);
});