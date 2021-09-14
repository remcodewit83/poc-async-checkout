import { UserIcon, ShoppingBagIcon, ChatIcon } from "@heroicons/react/outline";
import HeaderItem from "./HeaderItem";
import LogoUrl, { ReactComponent as Logo } from "../../public/logo.svg";
import Link from "next/link";

function Header() {
  return (
    <header className="flex flex-row m-5 justify-between items-center">
      <Link href="/">
        <img src={LogoUrl} className="object-contain cursor-pointer" />
      </Link>
      <div className="flex justify-evenly max-w-2xl">
        <pre className="hidden font-sans sm:block">1-844-430-6453</pre>
        <HeaderItem Icon={ChatIcon} />
        <HeaderItem Icon={ShoppingBagIcon} />
        <HeaderItem Icon={UserIcon} />
      </div>
    </header>
  );
}

export default Header;
