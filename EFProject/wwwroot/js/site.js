let isCollapsed = false;

function toggleSidebar() {
    isCollapsed = !isCollapsed;
    const sidebar = document.getElementById('sidebar');
    const content = document.getElementById('content');

    // Toggle the collapsed class for the sidebar
    sidebar.classList.toggle('collapsed', isCollapsed);
    content.classList.toggle('expanded', isCollapsed);
}

$('#sidebar .nav-link').each(function () {
    if (this.href === window.location.href) {
        $(this).addClass('active');
    }
});

$('#sidebar .nav-link').on('click', function (e) {
    e.preventDefault(); // Stop immediate navigation
    $('#sidebar .nav-link').removeClass('active');
    $(this).addClass('active');
    window.location.href = $(this).attr('href'); // Navigate
});
