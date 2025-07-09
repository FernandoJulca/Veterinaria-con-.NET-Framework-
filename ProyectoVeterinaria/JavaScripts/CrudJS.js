document.getElementById('imagenInput').addEventListener('change', function (evt) {
    const file = evt.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            const base64String = e.target.result.split(',')[1];
            document.getElementById('ImagenBase64').value = base64String;
            document.getElementById('preview').src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});