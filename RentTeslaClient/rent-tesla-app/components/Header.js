import React from 'react';
import Link from 'next/link';
import { useRouter } from 'next/router';

const Header = () => {
    const router = useRouter();
    const isHome = router.pathname === '/';

    return ( 
            <header className='container-header'>
                <nav className='nav'>
                   <h3 className='nav-text'>Rental Tesla Cars</h3>
                   {!isHome && (
                        <Link className='nav-link' href="/">
                        Back to Home
                        </Link>
                    )}
                </nav>
            </header>
     );
}
 
export default Header;