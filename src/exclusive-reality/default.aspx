<%@ Page Language="C#" %>
<script runat="server">
  protected override void OnLoad(EventArgs e)
  {
    Response.Redirect("index.aspx", true);
    base.OnLoad(e);
  }
</script>
