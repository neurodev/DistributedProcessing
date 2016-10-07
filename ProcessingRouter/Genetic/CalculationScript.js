
var currentPosition = 0;
var comparisonImage = new Image();
var generationCanvas = document.createElement("canvas");
var generationCanvasHidden = document.createElement("canvas");
var comparisonCanvas = document.createElement("canvas");
var generationContext = generationCanvas.getContext("2d");
var ImageInitialized = false;
    
var workingParameters;




function StartCalculation(parameters) {
    workingParameters = parameters;
    if (ImageInitialized) {
        StartGeneration();
    } else {
        comparisonImage.onload = function () { comparisonImageLoaded(); };
        comparisonImage.src = "[replace#targetImageURL]";
    }
}

    function plotLine(x0, y0, x1, y1,colour) {
        var dx = Math.abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
        var dy = -Math.abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
        var err = dx + dy, e2;                                   /* error value e_xy */

        for (; ;) {                                                          /* loop */
            SetPixel(x0, y0, colour);
            if (x0 == x1 && y0 == y1) break;
            e2 = 2 * err;
            if (e2 >= dy) { err += dy; x0 += sx; }                        /* x step */
            if (e2 <= dx) { err += dx; y0 += sy; }                        /* y step */
        }
    }

function SetPixel(x, y, colour) {
    generationContext.fillStyle = "#" + colour;
    generationContext.fillRect(x, y, 1, 1);
}


function comparisonImageLoaded() {
    ImageInitialized = true;
    comparisonCanvas.width = comparisonImage.width;
    comparisonCanvas.height = comparisonImage.height;
    generationCanvas.width = comparisonImage.width;
    generationCanvas.height = comparisonImage.height
    generationCanvasHidden.width = comparisonImage.width;
    generationCanvasHidden.height = comparisonImage.height

    var comparisonContext = comparisonCanvas.getContext("2d");

    comparisonContext.drawImage(comparisonImage, 0, 0);
    document.body.appendChild(comparisonCanvas);
    document.body.appendChild(generationCanvas);
    StartGeneration();
}

function StartGeneration() {
    generationContext.fillStyle = "#FFFFFF";
    generationContext.fillRect(0, 0, generationCanvas.width, generationCanvas.height);
    
    currentPosition = 0;
    DrawStep();
}

function DrawStep() {
    if (currentPosition >= workingParameters.steps.length) {
        DoComparison();
    } else {
        var currentStep = workingParameters.steps[currentPosition];
        plotLine(currentStep.StartX, currentStep.StartY, currentStep.EndX, currentStep.EndY, currentStep.colour);
        //generationContext.beginPath();
        //generationContext.strokeStyle = currentStep.colour;
        //generationContext.moveTo(currentStep.StartX, currentStep.StartY);
        //generationContext.lineTo(currentStep.EndX, currentStep.EndY);
        //generationContext.stroke();
        currentPosition += 1;
        setTimeout(DrawStep, [replace#delay]);
    }

}



function DoComparison() {
    var generation = generationContext.getImageData(0, 0, generationCanvas.width, generationCanvas.height);
    var generationPixels = generation.data;
    var comparisonContext = comparisonCanvas.getContext("2d");
    var comparison = comparisonContext.getImageData(0, 0, comparisonCanvas.width, comparisonCanvas.height);
    var comparisonPixels = comparison.data;

    var score = 0;

    for (var i = 0, n = generationPixels.length; i < n; i += 4) {
        if (generationPixels[i] === comparisonPixels[i]
            && generationPixels[i+1] === comparisonPixels[i+2]
            && generationPixels[i+2] === comparisonPixels[i+2]
            ) {
            score++;
        } else {
            generationPixels[i] = 0;
            generationPixels[i + 1] = 0;
            generationPixels[i + 2] = 255;
        }
    }

// Draw the ImageData at the given (x,y) coordinates.
    generationContext.putImageData(generation, 0, 0);
    //alert("score: " + score);
    ReturnResult(workingParameters.ParameterSetId,score);
}