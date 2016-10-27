var requestorName = '[replace#requestorName]';

function ReturnResult(parametersetID, result) {
    var userId = '[replace#workerId]';
    loadJSON("[replace#parameterURL]/RegisterResult","WorkerId=" +userId + "&ParameterSetId=" + parametersetID + "&Result="+result + "&Requestor="+requestorName,
     function (data) { setTimeout(function () { StartCalculation(data); }, 5); },
        function(xhr) { console.error(xhr); });
}


function loadJSON(path,params, success, error) {
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                if (success)
                    success(JSON.parse(xhr.responseText));
            } else {
                if (error)
                    error(xhr);
            }
        }
    };
    xhr.open("POST", path, true);
    xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xhr.send(params);
}

function GetNewParameterSet() {
    alert('Getting new parameterset');
    var userId = '[replace#workerId]';
    loadJSON("[replace#parameterURL]/GetNewParameters", "WorkerId=" +userId + "&Requestor=" + requestorName,
        function (data) { StartCalculation(data); },
        function(xhr) { console.error(xhr); })
    

    //TODO : Ajax post for parameters
    //alert("GetNewParameterSet");
    //StartCalculation(parameters);
}

document.addEventListener("DOMContentLoaded", function (event) {
    GetNewParameterSet();
});

