﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var formHandle1 = document.forms.newform;
if (formHandle1 != null)
    formHandle1.onsubmit = passForm1;
var formHandle2 = document.forms.updateform;
if (formHandle2 != null)
    formHandle2.onsubmit = passForm2;

function passForm1() {
    return processForm(formHandle1);
}

function passForm2() {
    return processForm(formHandle2);
}

function processForm(formHandle) {
    const TeacherFname = formHandle.TeacherFname;
    const TeacherLname = formHandle.TeacherLname;
    const EmployeeNumber = formHandle.EmployeeNumber;
    const HireDate = formHandle.HireDate;
    const Salary = formHandle.Salary;
    const ErrFname = document.getElementById("err-fname");
    const ErrLname = document.getElementById("err-lname");
    const ErrNumber = document.getElementById("err-number");
    const ErrDate = document.getElementById("err-date");
    const ErrSalary = document.getElementById("err-salary");
    var validate = false;

    var nameCheck = /[A-Za-z]/;
    var numberCheck = /^[T]\d{3}$/;
    var dateCheck = /^[12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])\s(0[0-9]|1[0-2]):[0-5]\d:[0-5]\d$/;
    var salaryCheck = /^[0-9]\d{0,9}(\.\d{1,2})?%?$/;

    resetErr(ErrFname);
    resetErr(ErrLname);
    resetErr(ErrNumber);
    resetErr(ErrDate);
    resetErr(ErrSalary);

    if (TeacherFname.value === "")
        validate = emptyInput(ErrFname, "First name");
    else if (!nameCheck.test(TeacherFname.value))
        validate = wrongInput(ErrFname, "characters only");
    if (TeacherLname.value === "")
        validate = emptyInput(ErrLname, "Last name");
    else if (!nameCheck.test(TeacherLname.value))
        validate = wrongInput(ErrLname, "characters only");
    if (EmployeeNumber.value === "")
        validate = emptyInput(ErrNumber, "Employee number");
    else if (!numberCheck.test(EmployeeNumber.value))
        validate = wrongInput(ErrNumber, "T follows with 3 numbers");
    if (HireDate.value === "")
        validate = emptyInput(ErrDate, "Hire date");
    else if (!dateCheck.test(HireDate.value))
        validate = wrongInput(ErrDate, "YYYY-MM-DD HH:MM:SS");
    if (Salary.value === "")
        validate = emptyInput(ErrSalary, "Salary");
    else if (!salaryCheck.test(Salary.value))
        validate = wrongInput(ErrSalary, "numbers only (at most 2 decimal placess)");

    if (validate === true)
        return false;
}

function emptyInput(ErrMsg, Name) {
    ErrMsg.innerHTML = Name + " is required.";
    return true;
}

function wrongInput(ErrMsg, Format) {
    ErrMsg.innerHTML = "Please input " + Format + ".";
    return true;
}

function resetErr(ErrMsg) {
    ErrMsg.innerHTML = "";
}