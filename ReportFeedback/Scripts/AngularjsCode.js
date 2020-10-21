var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    Clear();
    function Clear() {
        $scope.Post = {};
        $scope.Post.PostId = 0;
        $scope.Comment = {};
        $scope.Comment.CommentId = 0;
        $scope.PostList = [];
        $scope.CommentList = [];
        GetAllComment();
        GetAllPost();
        
    }


   
    function GetAllComment() {
        $http({
            method: "get",
            url: "http://localhost:6814/api/Userpost/UserComment"
        }).then(function (response) {
            $scope.CommentList = response.data;
            console.log($scope.CommentList);
        }, function () {
            alert("Error Occur");
        })
    }
    function GetAllPost() {
        $http({
            method: "get",
            url: "http://localhost:6814/api/Userpost/UserPost"
        }).then(function (response) {
            $scope.PostList = response.data;
            console.log($scope.PostList);
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.SaveAndGetLikeValue = function (comment) {
        comment.LikeCount = comment.LikeCount + 1;
        $http({
            method: "post",
            url: "http://localhost:6814/api/Userpost/UserCommentUpdate",
            datatype: "json",
            data: JSON.stringify(comment)
        }).then(function (response) {
            console.log($scope.PostList);
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.SaveAndGetDislikeValue = function (comment) {
        if (comment.DislikeCount > 0) {
            comment.DislikeCount = comment.DislikeCount - 1;
        }
        $http({
            method: "post",
            url: "http://localhost:6814/api/Userpost/UserCommentUpdate",
            datatype: "json",
            data: JSON.stringify(comment)
        }).then(function (response) {
            console.log($scope.PostList);
        }, function () {
            alert("Error Occur");
        })
    }

    $scope.GetPagedMore = function () {
        $http({
            method: "get",
            url: "http://localhost:6814/api/Userpost/GetPagedMore"
        }).then(function (response) {
            $scope.PostList = response.data;
            console.log($scope.PostList);
        }, function () {
            alert("Error Occur");
        })
    }
    $scope.GetPagedMinus = function () {
        $http({
            method: "get",
            url: "http://localhost:6814/api/Userpost/GetPagedMinus"
        }).then(function (response) {
            $scope.PostList = response.data;
            console.log($scope.PostList);
        }, function () {
            alert("Error Occur");
        })
    }
})  