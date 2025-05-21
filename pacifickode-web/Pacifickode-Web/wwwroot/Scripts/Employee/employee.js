$(document).ready(function () {

    // Initialize form validation
    $("#employees").validate({
        ignore: ":hidden:not(select)",
        errorClass: "my-error-class form-control-danger",
        validClass: "my-valid-class",

        rules: {
            'first-name': { required: true },
            'last-name': { required: true },
            'email': { required: true, email: true },
            'date-of-birth': { required: true },
            'salary': { required: true, number: true },
            'department-id': { required: true }
        },
        messages: {
            'first-name': "First name is required",
            'last-name': "Last name is required",
            'email': {
                required: "Email is required",
                email: "Enter a valid email"
            },
            'date-of-birth': "Date of birth is required",
            'salary': {
                required: "Salary is required",
                number: "Enter a valid number"
            },
            'department-id': "Please select a department"
        },

        submitHandler: function (form) {
            let btnText = $("#btn-save").text();
            if (btnText === "Update") {
                updateEmployee();
            } else {
                saveEmployee();
            }
        }
    });

    // Edit button handler
    $(document).on('click', '.edit-btn', function () {
        const row = $(this).closest('tr');
        $('#employee-id').val(row.find('td:eq(1)').text());
        $('#first-name').val(row.find('td:eq(2)').text());
        $('#last-name').val(row.find('td:eq(3)').text());
        $('#email').val(row.find('td:eq(4)').text());
        $('#date-of-birth').val(row.find('td:eq(5)').text());
        $('#age').val(row.find('td:eq(6)').text());
        $('#salary').val(row.find('td:eq(7)').text());
        $('#department-id').val(row.find('td:eq(8)').data("dept-id"));
        $("#btn-save").text('Update');
    });

    // Delete button handler
    $(document).on('click', '.delete-btn', function (e) {
        e.preventDefault();
        const employeeId = $(this).data('id');
        if (confirm("Are you sure you want to delete this employee?")) {
            $.ajax({
                url: window.base_url + 'api/Employee/deleteemployee/' + employeeId,
                type: 'DELETE',
                success: function (res) {
                    alert(res.message);
                    getAllEmployees();
                    clearEmployeeForm();
                },
                error: function () {
                    alert("Failed to delete employee.");
                }
            });
        }
    });

    // Clear form
    $('#btn-new').on('click', function () {
        clearEmployeeForm();
    });

    // Auto-calculate age from DOB
    $('#date-of-birth').on('change', function () {
        const dob = new Date(this.value);
        const age = new Date().getFullYear() - dob.getFullYear();
        $('#age').val(age);
    });

    // Initial load
    loadDepartments();
    getAllEmployees();
});

// Load departments into dropdown
function loadDepartments() {
    $.ajax({
        url: window.base_url + 'api/Department/loadalldepartment',
        type: 'GET',
        success: function (data) {
            const ddl = $('#department-id');
            ddl.empty().append('<option value="">Select department</option>');
            $.each(data, function (i, d) {
                ddl.append(`<option value="${d.departmentId}">${d.departmentName}</option>`);
            });
        }
    });
}

// Get all employees
function getAllEmployees() {
    $.ajax({
        url: window.base_url + 'api/Employee/loadallemployees',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tbody = $("#employeeBody");
            tbody.empty();

            $.each(data, function (index, e) {
                var row = `
                    <tr>
                        <td>${index + 1}</td>
                        <td class="d-none">${e.employeeId}</td>
                        <td>${e.firstName}</td>
                        <td>${e.lastName}</td>
                        <td>${e.email}</td>
                        <td>${e.dateOfBirth.split('T')[0]}</td>
                        <td>${e.age}</td>
                        <td>${e.salary}</td>
                        <td data-dept-id="${e.departmentId}">${e.departmentName}</td>
                        <td>
                            <button class="btn btn-sm btn-outline-primary me-1 edit-btn">Edit</button>
                            <button class="btn btn-sm btn-outline-danger delete-btn" data-id="${e.employeeId}">Delete</button>
                        </td>
                    </tr>`;
                tbody.append(row);
            });
        },
        error: function () {
            console.error("Failed to load employees.");
        }
    });
}

// Save new employee
function saveEmployee() {
    var employee = {
        EmployeeId: $('#employee-id').val(),
        FirstName: $('#first-name').val(),
        LastName: $('#last-name').val(),
        Email: $('#email').val(),
        DateOfBirth: $('#date-of-birth').val(),
        Age: $('#age').val(),
        Salary: $('#salary').val(),
        DepartmentId: $('#department-id').val(),
        DepartmentName:''
    };
    $.ajax({
        url: window.base_url + 'api/Employee/saveemployee',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(employee),
        success: function (res) {
            alert(res.message);
            getAllEmployees();
            clearEmployeeForm();
        },
        error: function () {
            alert("Error saving employee.");
        }
    });
}

// Update existing employee
function updateEmployee() {
    var employee = {
        EmployeeId: $('#employee-id').val(),
        FirstName: $('#first-name').val(),
        LastName: $('#last-name').val(),
        Email: $('#email').val(),
        DateOfBirth: $('#date-of-birth').val(),
        Age: $('#age').val(),
        Salary: $('#salary').val(),
        DepartmentId: $('#department-id').val(),
        DepartmentName: ''
    };

    $.ajax({
        url: window.base_url + 'api/Employee/updateemployee',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(employee),
        success: function (res) {
            alert(res.message);
            getAllEmployees();
            clearEmployeeForm();
        },
        error: function () {
            alert("Error updating employee.");
        }
    });
}

// Clear employee form
function clearEmployeeForm() {
    $('#employee-id').val(0);
    $('#first-name, #last-name, #email, #date-of-birth, #age, #salary').val("");
    $('#department-id').val("");
    $("#btn-save").text('Save');

    var validator = $("#employees").validate();
    validator.resetForm();
}
