import React from 'react';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'
import {faCarSide} from "@awesome.me/kit-7f69c3900d/icons/sharp/thin";

export default function Navbar() {
    return (
        <header className={`sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md`}>
            <div className={`flex items-center gap-2 text-3xl font-semibold text-red-500`}>
                <FontAwesomeIcon icon={faCarSide} className={`font-semibold`}/>
                <div>Carsties Auctions</div>
            </div>
            <div>Search</div>
            <div>Login</div>
        </header>
    );
}