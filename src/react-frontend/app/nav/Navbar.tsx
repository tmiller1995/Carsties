import React from 'react';
import {FontAwesomeIcon} from '@fortawesome/react-fontawesome'
import {faCarSide} from "@awesome.me/kit-7f69c3900d/icons/sharp/thin";

export default function Navbar() {
    return (
        <header className={`sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md`}>
            <FontAwesomeIcon icon={faCarSide}/>
            <div>Carsties Auctions</div>
            <div>Middle</div>
            <div>Right</div>
        </header>
    );
}