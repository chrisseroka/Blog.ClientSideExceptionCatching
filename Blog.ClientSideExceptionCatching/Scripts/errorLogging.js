(function() {
    var errorTreshold = 0;
    var maxErrorTreshold = 10;

    window.onerror = function (errorMsg, url, lineNumber, columnNumber, errorObject) {
        if (errorTreshold++ > maxErrorTreshold) {
            return;
        }

        var arg = {
            message: errorMsg,
            url: url,
            line: lineNumber,
            column: columnNumber
        };
        if (errorObject) {
            arg.stack = errorObject.stack;
        }

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/JavaScriptError/Log');

        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.send(JSON.stringify(arg));
    };
})();