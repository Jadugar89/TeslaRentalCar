import React from "react";
import { Button, buttonVariants } from "./ui/button";
import Link from "next/link";

export default function Navbar() {
  return (
    <nav
      className="relative flex w-full items-center justify-between bg-white py-2 shadow-sm shadow-neutral-700/10 dark:bg-neutral-800 dark:shadow-black/30 lg:flex-wrap lg:justify-start"
      data-te-navbar-ref
    >
      <div className="flex w-full flex-wrap items-center justify-between px-6">
        <Link className={buttonVariants({ variant: "ghost" })} href="/">
          Tesla RentalCar
        </Link>
        <div className="my-1 flex items-center lg:my-0 lg:ml-auto">
          <Button data-te-ripple-init data-te-ripple-color="light">
            Login
          </Button>
          <Button variant="secondary" className="ml-2">
            Sign up
          </Button>
        </div>
      </div>
    </nav>
  );
}
