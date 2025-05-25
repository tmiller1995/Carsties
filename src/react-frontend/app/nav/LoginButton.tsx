"use client";

import { Button } from "flowbite-react";
import { signIn } from "next-auth/react";

export default function LoginButton() {
  return (
    <Button
      className="cursor-pointer"
      outline
      onClick={() => signIn("id-server")}
    >
      Login
    </Button>
  );
}
