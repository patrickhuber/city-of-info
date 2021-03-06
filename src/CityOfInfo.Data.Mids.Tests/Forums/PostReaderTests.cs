﻿using CityOfInfo.Data.Mids.Forums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CityOfInfo.Data.Mids.Tests.Forums
{
    public class PostReaderTests
    {
        [Fact]
        public void CanReadPost()
        {
            var postData = @"| Copy & Paste this data into Mids' Reborn : Hero Designer to view the build |
|-------------------------------------------------------------------|
|MxDz;1719;775;1550;HEX;|
|78DA6D94C9531A4114C67BA011415611C41D01F788A0C653AA8C7B545C129378A45|
|0C7382582056AC5CA9ECA2195C4E490432ED9AEB9E43FCB52D9ABB290E7BC4F980A|
|992AEA377CFDBE7EDDFDFACDC2D549C7DB993BA342714D64D285426A359D2F6CA53|
|754EBCABAA666D755418F6751DBDED64253D98DD04C7A474D0D5A496C3B894C2D6D|
|6EAAD98276A0C62EEDACE5D399D438CDB4D75A1A9F54FF191FDBCFA7BDB3D92D35A|
|F66F762272FF6E55C2E135BD955D50D87FE9A54D3BB5AF68A57FF339B3DD00ADA9A|
|96D1F60E6B75653A975F5753B9CDD4AA96C988202D2941BFBB0E81A728C41982146|
|2BFAC2922A8086193C23568D67940630AC6947A1EF304882D5284699F667DB06832|
|F71D27904282D5E03DF25B90CFF2C624449D1401F259A159FD3441508A08093668B|
|67EF6DE276F8D9EBDA8D41CB2E6C8310F69CC897827E670D11C6E686E680F28CECB|
|9AD9BB4C084B517B1EBCC00CAC30AF51AC0FB1BE2F663D4FDD57F007B3EB3BF33AC|
|5FAB136FF9149D7024F993768AC1EEBA86FE7F53E24AD8135D9B0C0718D497091D9|
|3F0F2E316F92A709F3348D70AD1E91D682795A4E735CEB08B37D983930040E328FC|
|8D3C61E4BDB186BA1B3E028388E393E819F99B7C81BE67D8A306A7A9BB40ED664C7|
|138EEBC4F974E27C3A717E5DDF988FC9D38D75773F97BAD6FB027CC93CF50A7CCD3|
|40584E8C3FEFB90DB4D358E418BA1C64ED2E2D0E2D09A65E94E9B05EA9E40DD13A8|
|7B02750FE9B1FA7E8ABDD2D01F457A44BC424954284315CA70853267541C7C6A498|
|9C4C79A8D349714CBB2D4714289F2AE6D1EEA34D68A17A5A1539AB84BDFD9C98379|
|1474CF87B2A628ED1C3770993BF7BD31BE9BE33F1AB567DCA94E59EA5E6989F19AA|
|BE24CFB00B3A79FD99B60BAA4A1E3F1857197358BE51CAA9B64CA79A67D8EE99AC5|
|2D58607A8CDE7178A7995553CCEA49A663029E19A659963A5AF81AF1852A6B161F6|
|E461037A31937A3153723E2C72DF6312DBA57616F91F71BFCC36CFE856EFCCD8CFC|
|44370BF65649C3D7057923C81B45DE28F25A8DEBC65DA9FE8F3F0A7F0FFC3DF0FF0|
|5846EE767|
|-------------------------------------------------------------------| ";
            var reader = new StringReader(postData);
            var postReader = new PostReader(reader);
            var post = postReader.Read();
            Assert.NotNull(post);
            Assert.NotNull(post.CompressionData);
            Assert.Equal(1719, post.CompressionData.UncompressedByteCount);
            Assert.Equal(775, post.CompressionData.CompressedByteCount);
            Assert.Equal(1550, post.CompressionData.EncodedByteCount);
            var expectedEncodedString = "78DA6D94C9531A4114C67BA011415611C41D01F788A0C653AA8C7B545C129378A450C7382582056AC5CA9ECA2195C4E490432ED9AEB9E43FCB52D9ABB290E7BC4F980A992AEA377CFDBE7EDDFDFACDC2D549C7DB993BA342714D64D285426A359D2F6CA53754EBCABAA666D755418F6751DBDED64253D98DD04C7A474D0D5A496C3B894C2D6D6EAAD98276A0C62EEDACE5D399D438CDB4D75A1A9F54FF191FDBCFA7BDB3D92D35AF66F762272FF6E55C2E135BD955D50D87FE9A54D3BB5AF68A57FF339B3DD00ADA9A96D1F60E6B75653A975F5753B9CDD4AA96C988202D2941BFBB0E81A728C419821462BFAC2922A8086193C23568D67940630AC6947A1EF304882D5284699F667DB06832F71D27904282D5E03DF25B90CFF2C624449D1401F259A159FD3441508A08093668B67EF6DE276F8D9EBDA8D41CB2E6C8310F69CC897827E670D11C6E686E680F28CECB9AD9BB4C084B517B1EBCC00CAC30AF51AC0FB1BE2F663D4FDD57F007B3EB3BF33AC5FAB136FF9149D7024F993768AC1EEBA86FE7F53E24AD8135D9B0C0718D497091D93F0F2E316F92A709F3348D70AD1E91D682795A4E735CEB08B37D983930040E328FC8D3C61E4BDB186BA1B3E028388E393E819F99B7C81BE67D8A306A7A9BB40ED664C7138EEBC4F974E27C3A717E5DDF988FC9D38D75773F97BAD6FB027CC93CF50A7CCD340584E8C3FEFB90DB4D358E418BA1C64ED2E2D0E2D09A65E94E9B05EA9E40DD13A87B02750FE9B1FA7E8ABDD2D01F457A44BC424954284315CA70853267541C7C6A4989C4C79A8D349714CBB2D4714289F2AE6D1EEA34D68A17A5A1539AB84BDFD9C983791474CF87B2A628ED1C3770993BF7BD31BE9BE33F1AB567DCA94E59EA5E6989F19AABE24CFB00B3A79FD99B60BAA4A1E3F1857197358BE51CAA9B64CA79A67D8EE99AC52D58607A8CDE7178A7995553CCEA49A663029E19A659963A5AF81AF1852A6B161F6E461037A31937A3153723E2C72DF6312DBA57616F91F71BFCC36CFE856EFCCD8CFC44370BF65649C3D7057923C81B45DE28F25A8DEBC65DA9FE8F3F0A7F0FFC3DF0FF05846EE767";
            Assert.Equal(expectedEncodedString, post.CompressionData.EncodedString);
        }
    }
}
