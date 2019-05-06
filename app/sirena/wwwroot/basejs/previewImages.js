function preview_image(event, imgTagId) {
    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById(imgTagId);
        output.src = reader.result;
    }
    reader.readAsDataURL(event.target.files[0]);
}