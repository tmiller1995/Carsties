import NextAuth, { Profile } from "next-auth";
import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";
import { OIDCConfig } from "@auth/core/providers";

export const { handlers, signIn, signOut, auth } = NextAuth({
  debug: true,
  providers: [
    DuendeIDS6Provider({
      id: "id-server",
      clientId: "frontend",
      clientSecret: "SuperSecretDooDooDoo",
      issuer: "http://localhost:7018",
      authorization: { params: { scope: "openid profile auctionApp" } },
      idToken: true,
    } as OIDCConfig<Profile>),
  ],
});
