$.validator.addMethod('filesize', function (value, element, param) { /*validator for file size*/
    return isValid = this.optional(element) || element.file[0].size <= param
});