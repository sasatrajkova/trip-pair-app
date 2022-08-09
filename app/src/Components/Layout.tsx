import React from "react";
import Header from "./Header";

interface Props {
  children: React.ReactNode;
}
const Layout: React.FC<Props> = (props: Props) => {
  return (
    <div className="container mx-auto">
      <Header />
      <main>{props.children}</main>
    </div>
  );
};

export default Layout;
