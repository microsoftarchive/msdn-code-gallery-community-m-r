import React from 'react';

import '../../styles/app.scss';

const NavPages = ({totalCount, currentPage, pageSize, pageStyles}) => {
    const pages = [];
    for (let i = 0; i < totalCount / pageSize; i++) pages.push(i + 1);

    const classes = 'page-item btn btn-sm cus-font-xxs';
    const navClasses = `${classes} btn-light`;
    const numberClasses = (page) => `${classes} ${
                            page === currentPage
                            ? 'btn-primary'
                            : 'btn-light'}`;

    return (
        <nav aria-label="Page navigation for styles by category">
            <div className="mt-5 pagination justify-content-center">
                <button value={1} className={navClasses} onClick={pageStyles}>
                    &lsaquo;
                </button>
                {pages.map(page => (
                    <button key={page} value={page} className={numberClasses(page)}
                            onClick={pageStyles}>
                        {page}
                    </button>
                ))}
                <button value={Math.max(...pages)} className={navClasses} onClick={pageStyles}>
                    &rsaquo;
                </button>
            </div>
        </nav>
    );
}

export default NavPages;